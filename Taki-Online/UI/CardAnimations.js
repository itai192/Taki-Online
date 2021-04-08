function pageLoad() {
    animate();
}
function animate() {
    var ToDraw = document.getElementsByClassName("Draw");
    var ToPut= document.getElementsByClassName("Put");
    var Pile = document.getElementById("Pile");
    var pilePosX = Pile.offsetLeft;
    var pilePosY = Pile.offsetTop;
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