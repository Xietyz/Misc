
var btnDebug = document.getElementById("btnDebug");
var lblDebug = document.getElementById("lblDebug");
var txtDebug = document.getElementById("txtDebug");

function debugButtonClicked() {
    var input = "";
    input = txtDebug.value;
    //debugger;
    lblDebug.innerText = "You have clicked the button";
    changeLabelText(input);
}

function changeLabelText(text) {
    var labelDisplay = "";
    labelDisplay = "Your input is: " + text;
    lblDebug.innerText = labelDisplay;
}

function buggedClick() {
    var firstNumber = 1;
    var secondNumber = "2 ";
    var label = document.getElementById("lblBugged");

    var combine = firstNumber + secondNumber;
    label.innerHTML = combine;
}