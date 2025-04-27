 

document.addEventListener("DOMContentLoaded", function () {
    const buttons = document.querySelectorAll(".klik");

    buttons.forEach(button => {
        button.addEventListener("click", function (event) {
            event.preventDefault();

            const id = this.getAttribute("id");
            //console.log(id);
            otvoriPopup(id);
        });
    });
});

function otvoriPopup(id) {
    //console.log("Probao " + id)
    const popup = document.getElementById(`element-${id}`);
    const openBtn = document.getElementById(`openPopup-${id}`);
    const closeBtn = document.getElementById(`closePopup-${id}`);

    //console.log(popup.value + " sta je ovo");
    // Kada kliknemo na dugme "Login", prikazuje se popup sa animacijom
    openBtn.addEventListener("click", () => {
        if (!popup.classList.contains("show")) {
            popup.classList.add("show");
        }
    });

    // Kada kliknemo na "X", zatvara se popup
    closeBtn.addEventListener("click", () => {
        if (popup.classList.contains("show")) {
            popup.classList.remove("show");
        }
        
    });

    // Kada kliknemo van popup-a, takođe se zatvara
    window.addEventListener("click", (e) => {
        if (e.target === popup) {
            popup.classList.remove("show");
        }
    });
}