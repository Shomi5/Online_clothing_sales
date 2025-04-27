window.addEventListener("scroll", function() {
     let plaviDiv = document.getElementById("plaviDiv");
     let novaVisina = Math.max(10, 200 - window.scrollY);
     plaviDiv.style.height = novaVisina + "px";
});



