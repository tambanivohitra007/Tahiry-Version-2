$(document).ready(() => {
    $('.grid').hide();
});
//Fonction pour changer la parole
function _BibleVerse(text, verse, animation) {
    $('.grid').show();
    $('.bible').html(text);            
    animateCSS('.grid', animation);
    textFit($('.grid'));
    $('.verse').html(verse);
}       

// Function to animate text
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


function _Alignment(align) {
   $('#parent').css('text-align', align);    
}
function _Color(colorR, colorG, colorB) {
   $('.grid').css('color', `rgb(${colorR}, ${colorG}, ${colorB})`);    
}