document.addEventListener("DOMContentLoaded", () => {
    document.getElementById("execute").addEventListener("click", executeStringification);
});


const executeStringification = async () => {
    const string = document.getElementById("string").value;
    // todo - fetch and process data
    document.getElementById("result").textContent = string;
};