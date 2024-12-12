const element = document.querySelector('.grid');

// Function to change lyrics
function _Lyrics(text, animation, res) {
    console.log(text, animation, res); // Debug log
    $(".lyrics").css(res ? { "background-color": "" } : {
        "position": "absolute",
        "background-color": "rgba(5, 5, 5, 0.5)",
        "border-radius": "10px"
    });
    initAnimation(text, animation);
}

// Function to initialize animation
function initAnimation(text, animation) {
    if (!element) {
        console.error("Element '.grid' not found");
        return;
    }
    element.innerHTML = text;
    animateCSS('.grid', animation)
        .catch(console.error); // Catch potential errors in animation
    textFit($('.grid'));
}

// Function to change font
function _Font(font) {
    $('#parent').css("font-family", font);
}

// Function to increase font size
function IncreaseSize() {
    adjustFontSize(2);
}

// Function to decrease font size
function DecreaseSize() {
    adjustFontSize(-2);
}

// Helper function to adjust font size
function adjustFontSize(delta) {
    const grid = $(".grid");
    const currentFontSize = parseInt(grid.css("font-size"));
    const newFontSize = currentFontSize + delta + "px";
    console.log(newFontSize);
    $("#parent").css({ "font-size": newFontSize });
}

// Function to change image
function _Image(image) {
    $('.image-container').css('background-image', `url("${image}")`);
}

// Function to change video
function _Video(videoFile) {
    const videoSource = $('#parent video source');
    const currentSource = videoSource.attr('src');
    if (currentSource !== videoFile) {
        videoSource.attr('src', videoFile);
        $("#parent video")[0].load();
    }
}

// Function to change text alignment
function _Alignment(align) {
    $('#parent').css('text-align', align);
}

// Function to change text color
function _Color(colorR, colorG, colorB) {
    $('.grid').css('color', `rgb(${colorR}, ${colorG}, ${colorB})`);
}

// Function to check if text is italic
function isItalic() {
    const text = document.getElementById("myText");
    return text && text.style.fontStyle === 'italic';
}

// Function to check if text is bold
function isBold() {
    const grid = document.querySelector('.grid');
    if (!grid) {
        console.error("Element '.grid' not found");
        return false;
    }
    const style = window.getComputedStyle(grid);
    return style.getPropertyValue('font-weight') === 'bold';
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
