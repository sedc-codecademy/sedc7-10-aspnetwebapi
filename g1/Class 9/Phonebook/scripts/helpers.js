const baseUrl = 'http://localhost:61133'

const toggleView = (show, hide) => {
    show.classList.remove('hidden')
    hide.classList.add('hidden')
}

const getContacts = async () => {
    const token = sessionStorage.getItem('token')
    console.log(token)
    const response = await fetch(
        `${baseUrl}/api/contacts`,
        {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': token
            }
        }
    )
    return response.json()
}

const showContact = c => `
    <tr>
        <td>${c.firstName}</td>
        <td>${c.lastName}</td>
        <td>${c.email}</td>
        <td>${c.phonenumber}</td>
        <td>${c.address}</td>
    </tr>
`