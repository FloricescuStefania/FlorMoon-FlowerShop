 /* Increasing number of products */
document.addEventListener("DOMContentLoaded", function () {
    const qtyInput = document.getElementById('product-quantity');
    const hiddenQty = document.getElementById('hidden-quantity');
    const increaseBtn = document.getElementById('increase-qty');
    const decreaseBtn = document.getElementById('decrease-qty');

    if (increaseBtn && decreaseBtn && qtyInput) {
        increaseBtn.onclick = function () {
            let currentVal = parseInt(qtyInput.value) || 1;
            qtyInput.value = currentVal + 1;
            if (hiddenQty) hiddenQty.value = qtyInput.value;
        };

        decreaseBtn.onclick = function () {
            let currentVal = parseInt(qtyInput.value) || 1;
            if (currentVal > 1) {
                qtyInput.value = currentVal - 1;
                if (hiddenQty) hiddenQty.value = qtyInput.value;
            }
        };
    }
});

/* --- sorting categories --- */
function filter() {
    const catElement = document.getElementById("catFilter");
    const sortElement = document.getElementById("sortFilter");

    if (catElement && sortElement) {
        const cat = catElement.value;
        const sort = sortElement.value;

        window.location.href = `/Store?category=${encodeURIComponent(cat)}&sort=${sort}#catalog-section`;
    }
}

document.addEventListener("DOMContentLoaded", function () {
    if (window.location.hash === "#catalog-section") {
        const target = document.getElementById("catalog-section");
        if (target) target.scrollIntoView();
    }
});