window.scrollToSection = function (sectionId) {
        const section = document.getElementById(sectionId);
    if (section) {
        section.scrollIntoView({ behavior: 'smooth' });
        }
}

window.toggleFAQ = function (faqItem)
{
    const answer = faqItem.querySelector('.faq-answer');
    answer.style.display = answer.style.display === 'block' ? 'none' : 'block';
}

window.oneTwo = function () {
    console.log("animateParagraph called");
    const paragraph = document.getElementById("animated-paragraph");
    if (paragraph) {
        paragraph.classList.add("visible");
    }
}

window.typeOutText = function (text) {
    let index = 0;
        const interval = setInterval(() => {
            document.querySelector("#proof .proof-code pre").innerText += text[index];
            index++;
            if (index >= text.length) {
                clearInterval(interval);
            }
        }, 20); // Adjust the typing speed by changing the interval (milliseconds)
}