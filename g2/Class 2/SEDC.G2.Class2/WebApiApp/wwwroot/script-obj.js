let getAllBtn = document.getElementById("btn1");
let getByIdBtn = document.getElementById("btn2");
let addNoteBtn = document.getElementById("btn3");
let getAllTagsBtn = document.getElementById("btn4");
let getTagByIdBtn = document.getElementById("btn5");
let getByIdInput = document.getElementById("input2");
let addNoteInput1 = document.getElementById("input31");
let addNoteInput2 = document.getElementById("input32");
let addNoteInput3 = document.getElementById("input33");
let getAllTagsInput = document.getElementById("input4");
let getTagByIdInput1 = document.getElementById("input51");
let getTagByIdInput2 = document.getElementById("input52");

let port = "61566"
let getAllNotes = async () => {
    let url = "http://localhost:" + port + "/api/noteobjects";

    let response = await fetch(url);
    let data = await response.json();
    console.log(data);
};

let getNoteById = async () => {
    let url = "http://localhost:" + port + "/api/noteobjects/" + getByIdInput.value;

    let response = await fetch(url);
    let data = await response.text();
    console.log(data);
};

let addNote = async () => {
    let url = "http://localhost:" + port + "/api/noteobjects";
    let getTags = (str) => str.split(",").reduce(function (x, y, index) {
        let values = y.trim().split("-");
        x.push({ name: values[0], color: values[1] });
        return x;
    }, []);
    let note = { text: addNoteInput1.value, color: addNoteInput2.value, tags: getTags(addNoteInput3.value) }
    await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(note)
    });
}

let getAllTags = async () => {
    let url = "http://localhost:" + port + "/api/noteobjects/" + getAllTagsInput.value + "/tags";

    let response = await fetch(url);
    try {
        let data = await response.json();
        console.log(data);
    } catch (e) {
        console.log("Problem with request")
    }

};

let getTagById = async () => {
    let url = "http://localhost:" + port + "/api/noteobjects/" + getTagByIdInput1.value + "/tags/" + getTagByIdInput2.value;

    let response = await fetch(url);
    let data = await response.json();
    console.log(data);
};


getAllBtn.addEventListener("click", getAllNotes);
getByIdBtn.addEventListener("click", getNoteById);
addNoteBtn.addEventListener("click", addNote);
getAllTagsBtn.addEventListener("click", getAllTags);
getTagByIdBtn.addEventListener("click", getTagById);