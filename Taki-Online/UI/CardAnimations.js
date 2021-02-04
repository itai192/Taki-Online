function animate() {
    var ToDraw = document.getElementsByClassName("Draw");
    var ToPut= document.getElementsByClassName("Put");
    var Pile = document.getElementById("Pile");
    var Deck = document.getElementById("Deck");
    var pilePosX = Pile.style.left;
    var pilePosY = Pile.style.top;
    var DeckPosX = Deck.style.left;
    var DeckPosY = Deck.style.top
    var Deck = document.getElementById("Deck");
    for (var i = 0; i < ToDraw.length; i++)
    {
        var CardToDraw =ToDraw[i];
        var FinalPosX = CardToDraw.style.left;
        var FinalPosY = CardToDraw.style.top;
        CardToDraw.style.left=deckPosX;
        CardToDraw.style.top=deckPosY;
        CardToDraw.classList.add("AnimatableCard")
        CardToDraw.style.left=FinalPosX;
        CardToDraw.style.Top=finalPosY;
        if(CardToDraw.classList.contains(".DissapearingCard"))
        {
            CardToDraw.style.opacity=0;
        }
    }
    for (var i = 0; i < ToPut.length; i++)
    {
        var CardToPut = ToPut[i];
        CardToPut.classList.add("AnimatableCard")
        CardToPut.style.left=pilePosX;
        CardToPut.style.Top=finalPosY;
    }
}