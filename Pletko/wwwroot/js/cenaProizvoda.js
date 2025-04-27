document.addEventListener("DOMContentLoaded", function () {
    let kolicinaInput = document.getElementById("kolicina");
    let ukupnaCenaSpan = document.getElementById("ukupnaCena");
    let prikazKolicine = document.getElementById("prikazKolicine");

    // Preuzima cenu proizvoda iz data-cena atributa
    let cenaProizvoda = parseFloat(ukupnaCenaSpan.getAttribute("data-cena")) || 0;
    //console.log(cenaProizvoda);
    kolicinaInput.addEventListener("input", function () {
        let kolicina = parseInt(kolicinaInput.value) || 1;
        prikazKolicine.textContent = kolicina;
        ukupnaCenaSpan.textContent = (cenaProizvoda * kolicina).toFixed(2);
    });
});