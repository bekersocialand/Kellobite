function includeHTML() {
    var z, i, elmnt, file, xhttp;
    /* Loop through a collection of all HTML elements: */
    z = document.getElementsByTagName("*");
    for (i = 0; i < z.length; i++) {
        elmnt = z[i];
        /*search for elements with a certain atrribute:*/
        file = elmnt.getAttribute("w3-include-html");
        if (file) {
            /* Make an HTTP request using the attribute value as the file name: */
            xhttp = new XMLHttpRequest();
            xhttp.onreadystatechange = function () {
                if (this.readyState == 4) {
                    if (this.status == 200) {
                        elmnt.innerHTML = this.responseText;
                    }
                    if (this.status == 404) {
                        elmnt.innerHTML = "Page not found.";
                    }
                    /* Remove the attribute, and call this function once more: */
                    elmnt.removeAttribute("w3-include-html");
                    includeHTML();
                }
            };
            xhttp.open("GET", file, true);
            xhttp.send();
            /* Exit the function: */
            return;
        }
    }
}
includeHTML();
// fin includes

window.addEventListener('load', function () {

    // Menu code
    const collapse = document.getElementById('collapse');
    const menuContainer = document.querySelector('.menu-container');
    const menuNav = document.querySelector('.menu-nav');

    collapse.addEventListener('click', () => {
        console.log('click')
        menuContainer.classList.toggle('open');
        menuNav.classList.toggle('open');
    });

    document.addEventListener('click', (event) => {
        const target = event.target;
        if (!target.closest('.menu-container') && !target.closest('#collapse')) {
            menuContainer.classList.remove('open');
            menuNav.classList.remove('open');
        }
    });

});
