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
    let index = 0; const interval = setInterval(() => {
        const preElement = document.querySelector("#proof .proof-code pre");
        if (preElement) {
            preElement.innerText += text[index];
        }
        index++; if (index >= text.length) {
            clearInterval(interval);
        }
    }, 20);
}