let autoScrollInterval = null;
let currentY = 0;

window.startAutoScroll = function () {

    console.log("Auto Scroll iniciado");

    document.addEventListener("pointermove", handleMove);

    // Evita criar múltiplos intervals
    if (autoScrollInterval)
        return;

    autoScrollInterval = setInterval(() => {

        const scrollArea = 100;

        // Scroll para baixo
        if (currentY > window.innerHeight - scrollArea) {
            window.scrollBy(0, 15);
        }

        // Scroll para cima
        if (currentY < scrollArea) {
            window.scrollBy(0, -15);
        }

    }, 16); // Aproximadamente 60 FPS
};

window.stopAutoScroll = function () {

    console.log("Auto Scroll parado");

    document.removeEventListener("pointermove", handleMove);

    if (autoScrollInterval) {
        clearInterval(autoScrollInterval);
        autoScrollInterval = null;
    }
};

function handleMove(e) {
    currentY = e.clientY;
}