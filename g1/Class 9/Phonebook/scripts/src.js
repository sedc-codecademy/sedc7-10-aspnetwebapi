

const loginView = document.getElementById('loginView')
const loginForm = document.getElementById('loginForm')
const login = document.getElementById('login')
const loginUsername = document.getElementById('loginUsername')
const loginPassword = document.getElementById('loginPassword')

const registerView = document.getElementById('registerView')
const registerForm = document.getElementById('registerForm')
const register = document.getElementById('register')

loginView.addEventListener('click', () => toggleView(loginForm, registerForm))

registerView.addEventListener('click', () => toggleView(registerForm, loginForm))

login.addEventListener('click', async () => {
    const username = loginUsername.value
    const password = loginPassword.value

    const response = await fetch(`${baseUrl}/api/users/authenticate`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ username, password })
    })
    if (response.status === 401) {
        alert('Wrong username or password')
        return
    }
    const result = await response.json()
    alert(`Welcome to your phonebook ${result.fullName}`)
    sessionStorage.setItem('token', `Bearer ${result.token}`)
    toggleView(contactsView, auth)
    const contacts = await getContacts()
    const rows = contacts.reduce((str, c) => str += showContact(c), '')
    contactList.innerHTML = `
        ${rows}
    `

})

register.addEventListener('click', async () => {
    const firstName = regFirstName.value
    const lastName = regLastName.value
    const username = regUsername.value
    const password = regPassword.value
    const confirmPassword = regConfirmPassword.value

    const response = await fetch(`${baseUrl}/api/users/registeruser`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ username, password, firstName, lastName, confirmPassword })
    })
    // if (response.status === 401) {
    //     alert('Wrong username or password')
    //     return
    // }
    const result = await response.json()
    // alert(`Welcome to your phonebook ${result.fullName}`)
    console.log(result)

})

contactsView
