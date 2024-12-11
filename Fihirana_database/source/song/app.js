const element = document.querySelector('.grid');


//Fonction pour changer la parole
function _Lyrics(text, animation, res) {
    if (res) {
        $(".lyrics").css("background-color", "");
    }
    else {
        $(".lyrics").css("position", "absolute");
        $(".lyrics").css("background-color", "rgba(5, 5, 5, 0.5)");
        $(".lyrics").css("border-radius", "10px");      
    }
    initAnimation(text, animation);
}

function initAnimation(text, animation) {
    element.innerHTML = text;
    animateCSS('.grid', animation);
    textFit($('.grid'));
}

//Fonction pour changer la police
function _Font(font) {
    $('#parent').css("font-family", font);
    //localStorage.setItem('font', font);
}

//Fonction pour augmenter la tille de la police
function IncreaseSize() {
    var fontSize = parseInt($(".grid").css("font-size"));
    fontSize = fontSize + 2 + "px";
    console.log(fontSize);
    $("#parent").css({ 'font-size': fontSize });
}

//Fonction poour r�duire la taille de la police
function DecreaseSize() {
    var fontSize = parseInt($(".grid").css("font-size"));
    fontSize = fontSize - 2 + "px";
    console.log(fontSize);
    $("#parent").css({ 'font-size': fontSize });
}

//Fonction pour changer l'image
function _Image(image) {
    $('.image-container').css('background-image', 'url("' + image + '")');
    //localStorage.setItem('image', image);
}


function _Video(videoFile) {
    const currentSource = $('#parent video source').attr('src');
    if (currentSource !== videoFile) {
        $('#parent video source').attr('src', videoFile);
        $("#parent video")[0].load();
    }
}

//Fonction pour changer l'alignement du text
function _Alignment(align) {
    $('#parent').css('text-align', align);
    //localStorage.setItem('alignment', align);
}

//Fonction pour changer la couleur du texte en utilisant le mode RGB
function _Color(colorR, colorG, colorB) {
    $('.grid').css('color', `rgb(${colorR}, ${colorG}, ${colorB})`);
    //localStorage.setItem('color', `rgb(${colorR}, ${colorG}, ${colorB})`);
}

//Fonction pour retourner le style de la police 
function isItalic() {
    var text = document.getElementById("myText");
    return text.style.fontStyle === 'italic';
}
function isBold() {
    var style = window.getComputedStyle('.grid');
    return style.getPropertyValue('font-weight') === 'bold';
}

//Fonction pour animer le texte en injectant les mots-cl�s
const animateCSS = (element, animation, prefix = 'animate__') =>
    // We create a Promise and return it
    new Promise((resolve, reject) => {
        const animationName = `${prefix}${animation}`;
        const node = document.querySelector(element);

        node.classList.add(`${prefix}animated`, animationName);

        // When the animation ends, we clean the classes and resolve the Promise
        function handleAnimationEnd(event) {
            event.stopPropagation();
            node.classList.remove(`${prefix}animated`, animationName);
            resolve('Animation ended');
        }

    node.addEventListener('animationend', handleAnimationEnd, { once: true });
});