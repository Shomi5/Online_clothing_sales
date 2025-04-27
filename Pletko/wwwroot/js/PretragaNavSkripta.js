const navEl = document.querySelector('.skrolaj')

window.addEventListener('scroll', function () {


    if (window.scrollY >= 420) {
        navEl.classList.add("navbar-scrolled")


    } else if (window.scrollY < 420) {
        navEl.classList.remove("navbar-scrolled")

    }
});

document.addEventListener("DOMContentLoaded", function () {
    var kategorijaDiv = document.querySelector(".kate");
    var footer = document.querySelector("footer");

    window.addEventListener("scroll", function () {
        var kateRect = kategorijaDiv.getBoundingClientRect();
        var footerRect = footer.getBoundingClientRect();

        if (window.scrollY >= 420 && kateRect.bottom < footerRect.top) {
            kategorijaDiv.classList.add("pozicijaFix");
            kategorijaDiv.style.position = "sticky";
            kategorijaDiv.style.bottom = ""; // Resetuje bottom vrednost
        }
        // Kada skrolaš do footera, postavi je da ostane pri dnu
        else if (window.scrollY >= 420 && kateRect.bottom >= footerRect.top) {
            kategorijaDiv.style.position = "absolute";
            kategorijaDiv.style.bottom = "0"; 
        }
     
        else {
            kategorijaDiv.classList.remove("pozicijaFix");
            kategorijaDiv.style.position = ""; 
            kategorijaDiv.style.bottom = ""; 
        }
    });
});


document.addEventListener("DOMContentLoaded", function () {

    const speed = 40; // Možeš da menjaš brzinu ispisivanja

    window.addEventListener("load", function () {
        var element = document.getElementById("paragraf");
        if (!element) return;  // Ako element nije pronađen, izlazi iz funkcije

        var text = element.innerText; // Uzima tekst koji je u elementu
        element.innerHTML = ""; // Briše prethodni sadržaj iz elementa

        var index = 0;

        // Funkcija koja dodaje jedno slovo u element
        function writer() {
            if (index < text.length) {
                element.innerHTML += text.charAt(index); // Dodaje jedno slovo
                index++;
                setTimeout(writer, speed); // Poziva funkciju nakon što prođe vreme
            }
        }

        writer();  // Pokreće ispisivanje
    });

});
