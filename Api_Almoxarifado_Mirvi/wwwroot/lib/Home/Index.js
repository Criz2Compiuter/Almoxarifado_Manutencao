function discountQuantity(productId, productListId, currentQuantity) {
    var input = document.querySelector(`input[name='quantidade'][data-product='${productId}']`);
    var discountQuantity = parseInt(input.value);

    if (isNaN(discountQuantity) || discountQuantity <= 0) {
        alert("Por favor, insira um valor numérico válido e maior que zero no campo de quantidade.");
        input.value = '';
        return;
    }

    if (discountQuantity > currentQuantity) {
        alert("O valor a ser descontado é maior do que a quantidade atual.");
        input.value = '';
        return;
    }

    input.value = '';
    console.log(`Product ID: ${productId}, Discounted Quantity: ${discountQuantity}`);
}

function searchProducts(inputId, resultsId) {
    var input = document.getElementById(inputId);
    var filter = input.value.toLowerCase();
    var resultsDiv = document.getElementById(resultsId);
    var table = resultsDiv.querySelector('tbody');
    var rows = table.getElementsByTagName('tr');
    var anyMatch = false;
    for (var i = 0; i < rows.length; i++) {
        var descriptionCell = rows[i].getElementsByTagName('td')[0];
        if (descriptionCell) {
            var description = descriptionCell.textContent || descriptionCell.innerText;
            var display = description.toLowerCase().indexOf(filter) > -1 ? '' : 'none';
            rows[i].style.display = display;
            if (display === '') {
                anyMatch = true;
            }
        }
    }
    if (anyMatch && filter !== '') {
        resultsDiv.style.display = 'block';
    } else {
        resultsDiv.style.display = 'none';
    }
    if (filter === '') {
        table.style.display = 'none';
    } else {
        table.style.display = '';
    }

    localStorage.setItem(inputId, filter);
}

window.onload = function () {
    var input1 = document.getElementById("searchInput1");
    var input2 = document.getElementById("searchInput2");
    var filter1 = localStorage.getItem("searchInput1");
    var filter2 = localStorage.getItem("searchInput2");

    if (input1 && filter1) {
        input1.value = filter1;
        searchProducts("searchInput1", "search-results1");
    }

    if (input2 && filter2) {
        input2.value = filter2;
        searchProducts("searchInput2", "search-results2");
    }

    var quantityInputs = document.querySelectorAll(".quantity-input");
    quantityInputs.forEach(function (input) {
        input.addEventListener("blur", function () {
            var value = input.value.trim();
            if (value !== '' && !isNaN(value)) {
                input.value = parseInt(value);
            } else if (quantidadeInput > quantidadeDisponivel) {
                alert("Quantidade excede a quantidade disponível do produto!");
            } else {
                alert("Por favor, informe um número válido no campo de quantidade.");
                input.value = '';
            }
        });
    });
};