const element = document.querySelector('.grid');
//test
// Fonction pour changer la parole
function _Lyrics(text, animation, res) {
    console.log(text, animation, res); // Debug log to check values
    if (res) {
        $(".lyrics").css("background-color", "");
    } else {
        $(".lyrics").css({
            "position": "absolute",
            "background-color": "rgba(5, 5, 5, 0.5)",
            "border-radius": "10px"
        });
    }
    initAnimation(text, animation);
}


function initAnimation(text, animation) {
    if (element) {
        element.innerHTML = text;
        animateCSS('.grid', animation);
        textFit($('.grid'));
    } else {
        console.error("Element '.grid' not found");
    }
}

// Fonction pour changer la police
function _Font(font) {
    $('#parent').css("font-family", font);
}

// Fonction pour augmenter la taille de la police
function IncreaseSize() {
    var fontSize = parseInt($(".grid").css("font-size"));
    fontSize = fontSize + 2 + "px";
    console.log(fontSize);
    $("#parent").css({ 'font-size': fontSize });
}

// Fonction pour réduire la taille de la police
function DecreaseSize() {
    var fontSize = parseInt($(".grid").css("font-size"));
    fontSize = fontSize - 2 + "px";
    console.log(fontSize);
    $("#parent").css({ 'font-size': fontSize });
}

// Fonction pour changer l'image
function _Image(image) {
    $('.image-container').css('background-image', `url("${image}")`);
}

// Fonction pour changer la vidéo
function _Video(videoFile) {
    const currentSource = $('#parent video source').attr('src');
    if (currentSource !== videoFile) {
        $('#parent video source').attr('src', videoFile);
        $("#parent video")[0].load();
    }
}

// Fonction pour changer l'alignement du texte
function _Alignment(align) {
    $('#parent').css('text-align', align);
}

// Fonction pour changer la couleur du texte
function _Color(colorR, colorG, colorB) {
    $('.grid').css('color', `rgb(${colorR}, ${colorG}, ${colorB})`);
}

// Fonction pour vérifier si le texte est italique
function isItalic() {
    var text = document.getElementById("myText");
    return text.style.fontStyle === 'italic';
}

// Fonction pour vérifier si le texte est gras
function isBold() {
    var style = window.getComputedStyle(document.querySelector('.grid'));
    return style.getPropertyValue('font-weight') === 'bold';
}

// Fonction pour animer le texte
const animateCSS = (element, animation, prefix = 'animate__') =>
    new Promise((resolve, reject) => {
        const animationName = `${prefix}${animation}`;
        const node = document.querySelector(element);

        if (!node) {
            reject(`Element not found: ${element}`);
            return;
        }

        node.classList.add(`${prefix}animated`, animationName);

        function handleAnimationEnd(event) {
            event.stopPropagation();
            node.classList.remove(`${prefix}animated`, animationName);
            resolve('Animation ended');
        }

        node.addEventListener('animationend', handleAnimationEnd, { once: true });
    });
