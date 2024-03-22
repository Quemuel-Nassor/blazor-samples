export function scrollToActiveStep(stepId) {
    let step = document.getElementById(stepId);
    if (step != undefined && step != null)
        step.scrollIntoView({ behavior: "smooth", block: "end", inline: "nearest" });
}

export function setStepIndicatorWidth() {
    var root = document.querySelector(':root');
    var container = document.querySelector(".steps-container");
    if (container != null && container != undefined) {
        root.style.setProperty('--container-width', container.offsetWidth + 'px');
    }

    window.addEventListener("resize", function (e) {
        if (container != null && container != undefined) {
            root.style.setProperty('--container-width', container.offsetWidth + 'px');
        }
    });
}