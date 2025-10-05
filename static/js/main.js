document.addEventListener('DOMContentLoaded', function() {
    const sections = document.querySelectorAll('.section');
    const navDots = document.querySelectorAll('.nav-dot');
    const spotlights = document.querySelectorAll('.spotlight');
    const gridBackgrounds = document.querySelectorAll('.grid-background');
    const blueprintPath = document.getElementById('blueprint-path');

    // --- 1. SVG Blueprint Animation Setup ---
    if (blueprintPath) {
        const pathLength = blueprintPath.getTotalLength();
        blueprintPath.style.strokeDasharray = pathLength;
        blueprintPath.style.strokeDashoffset = pathLength;
    }

    // --- 2. Side Navigation & Section Activation Logic ---
    const sectionObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            const sectionId = entry.target.id;
            const correspondingDot = document.querySelector(`.nav-dot[data-section="${sectionId}"]`);

            if (entry.isIntersecting) {
                // Add active class to section for animations
                entry.target.classList.add('active');

                // Update nav dot
                navDots.forEach(dot => dot.classList.remove('active'));
                if (correspondingDot) {
                    correspondingDot.classList.add('active');
                }
            } else {
                // Remove active class when not intersecting
                entry.target.classList.remove('active');
            }
        });
    }, { threshold: 0.6 });

    sections.forEach(section => {
        sectionObserver.observe(section);
    });

    // Smooth scroll for nav links
    navDots.forEach(dot => {
        dot.addEventListener('click', function(e) {
            e.preventDefault();
            const targetId = this.getAttribute('href');
            document.querySelector(targetId).scrollIntoView({
                behavior: 'smooth'
            });
        });
    });


    // --- 3. Mouse Interaction Effects (Parallax and Spotlight) ---
    sections.forEach((section, index) => {
        section.addEventListener('mousemove', (e) => {
            const { clientX, clientY } = e;
            const { offsetWidth, offsetHeight } = section;

            const x = clientX - section.offsetLeft;
            const y = clientY - section.offsetTop;

            const currentSpotlight = spotlights[index];
            if(currentSpotlight) {
                currentSpotlight.style.background = `radial-gradient(circle at ${x}px ${y}px, transparent 100px, var(--bg-color) 350px)`;
            }

            const currentGrid = gridBackgrounds[index];
            if(currentGrid) {
                const moveX = (x / offsetWidth - 0.5) * 30;
                const moveY = (y / offsetHeight - 0.5) * 30;
                currentGrid.style.transform = `translate(${moveX}px, ${moveY}px)`;
            }
        });

        section.addEventListener('mouseleave', () => {
            const currentGrid = gridBackgrounds[index];
             if(currentGrid) {
                currentGrid.style.transform = 'translate(0, 0)';
             }
        });
    });
});