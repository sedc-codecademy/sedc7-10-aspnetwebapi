$(() => {
    fetchBookData();
    $("#search").on('click', searchBooks);
    $("#show-all").on('click', fetchBookData);
    $("#add-book").on('click', addNewBook);
    $("#edit-book").on('click', editBook);
});


const fetchBookData = async () => {
    const response = await fetch("/api/books");
    const data = await response.json();
    displayBooks(data);
};

const searchBooks = async () => {
    const title = $("#title").val();
    const response = await fetch(`/api/books/search/${title}`);
    const data = await response.json();
    displayBooks(data);
};

const displayBooks = (books) => {
    $("#table").html('');
    $("#table").append(`<tr>
    <td>ID</td>
    <td>Title</td>
    <td>Author</td>
    <td>Publication Year</td>
    <td>Edit</td>
    <td>Delete</td>
</tr>`);

    for (const book of books) {
        addBookRow(book);
    }
};


const addNewBook = async () => {
    // to-do: add client side validation
    const book = {
        id: 0,
        title: $("#new-title").val(),
        author: $("#new-author").val(),
        publicationYear: Number($("#new-year").val())
    };
    const response = await fetch("/api/books", {
        method: "POST",
        body: JSON.stringify(book),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    const addedBook = await response.json();
    addBookRow(addedBook);
};

const addBookRow = (book) => {
    $("#table").append(`<tr id="row-${book.id}">
    <td>${book.id}</td>
    <td>${book.title}</td>
    <td>${book.author}</td>
    <td>${book.publicationYear}</td>
    <td><button id="edit-${book.id}">Edit</button></td>
    <td><button id="delete-${book.id}">Delete</button></td>
</tr>`);

    $(`#edit-${book.id}`).on('click', startEditBook(book));
    $(`#delete-${book.id}`).on('click', deleteBook(book));
};

const deleteBook = (book) => async () => {
    const response = await fetch(`/api/books/${book.id}`, {
        method: "DELETE"
    });

    if (response.status === 200) {
        console.log("successfully deleted");
    } else {
        console.log("error deleting");
    }
    // to-do: remove it from the table
};

const startEditBook = (book) => async () => {
    $("#edit-id").val(book.id);
    $("#edit-title").val(book.title);
    $("#edit-author").val(book.author);
    $("#edit-year").val(book.publicationYear);
};

const editBook = async () => {
    // to-do: add client side validation
    const book = {
        id: $("#edit-id").val(),
        title: $("#edit-title").val(),
        author: $("#edit-author").val(),
        publicationYear: Number($("#edit-year").val())
    };
    const response = await fetch(`/api/books/${book.id}`, {
        method: "PUT",
        body: JSON.stringify(book),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    const editedBook = await response.json();
    // to-do: update it in the table
};