const navEl = document.querySelector('.skrolaj')

window.addEventListener('scroll', function () {


    if (window.scrollY >= 350) {
        navEl.classList.add("navbar-scrolled")


    } else if (window.scrollY < 350) {
        navEl.classList.remove("navbar-scrolled")

    }
});



document.addEventListener("DOMContentLoaded", function () {
    var kategorijaDiv = document.querySelector(".kate");
    var footer = document.querySelector("footer");

    window.addEventListener("scroll", function () {
        var kateRect = kategorijaDiv.getBoundingClientRect();
        var footerRect = footer.getBoundingClientRect();

        // Kada skrolaš do pozicije 490px i još nije došao do footera
        if (window.scrollY >= 490 && kateRect.bottom < footerRect.top) {
            kategorijaDiv.classList.add("pozicijaFix");
            kategorijaDiv.style.position = "sticky";
            kategorijaDiv.style.bottom = ""; // Resetuje bottom vrednost
        }
        // Kada skrolaš do footera, postavi je da ostane pri dnu
        else if (window.scrollY >= 490 && kateRect.bottom >= footerRect.top) {
            kategorijaDiv.style.position = "absolute";
            kategorijaDiv.style.bottom = "0"; // Prilagodi prema tome koliko želis da bude udaljena od footera
        }
        // Ako je skrolovan manje od 490px, resetuj poziciju
        else {
            kategorijaDiv.classList.remove("pozicijaFix");
            kategorijaDiv.style.position = ""; // Resetuje poziciju
            kategorijaDiv.style.bottom = ""; // Resetuje bottom vrednost
        }
    });
});

