function pageLoad() {
    animate();
}
function animate() {
    var ToDraw = document.getElementsByClassName("Draw");
    var ToPut= document.getElementsByClassName("Put");
    var Pile = document.getElementById("Pile");
    var Deck = document.getElementById("Deck");
    var pilePosX = Pile.offsetLeft;
    var pilePosY = Pile.offsetTop;
    var deckPosX = Deck.offsetLeft; 
    var deckPosY = Deck.offsetTop;
    var Deck = document.getElementById("Deck");
    for (var i = 0; i < ToDraw.length; i++)
    {
        var CardToDraw = ToDraw[i];
        var FinalPosX = CardToDraw.offsetLeft
        var FinalPosY = CardToDraw.offsetTop
        CardToDraw.classList.add("AnimatableCard")
        CardToDraw.style.left = deckPosX + "px";
        CardToDraw.style.top = deckPosY + "px";
        CardToDraw.style.left = FinalPosX + "px";
        CardToDraw.style.top = FinalPosY + "px";
    }
    for (var i = 0; i < ToPut.length; i++)
    {
        var CardToPut = ToPut[i];
        CardToPut.style.left = CardToPut.offsetLeft + "px";
        CardToPut.style.top = CardToPut.offsetTop + "px";
        CardToPut.classList.add("AnimatableCard")
        CardToPut.style.left=pilePosX+"px";
        CardToPut.style.top=pilePosY+"px";
    }
}