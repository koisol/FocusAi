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
