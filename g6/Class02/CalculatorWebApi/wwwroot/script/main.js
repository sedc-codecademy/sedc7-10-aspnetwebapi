document.addEventListener("DOMContentLoaded", () => {
    document.getElementById("execute").addEventListener("click", executeCalculation);
});


const executeCalculation = async () => {
    const first = document.getElementById("first").value;
    const second = document.getElementById("second").value;
    const op = document.getElementById("operation").value;

    const url = `/api/calculator/${op}/${first}/${second}`;

    const response = await fetch(url);
    const data = await response.json();
    document.getElementById("result").textContent = 
        `The result of ${data.operator} between ${data.firstArgument} and ${data.secondArgument} is ${data.result}`;
};