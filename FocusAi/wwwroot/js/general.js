window.scrollToSection = function (sectionId) {
    console("OK");
    console(sectionId);
        const section = document.getElementById(sectionId);
    if (section) {
        section.scrollIntoView({ behavior: 'smooth' });
        }
    }
