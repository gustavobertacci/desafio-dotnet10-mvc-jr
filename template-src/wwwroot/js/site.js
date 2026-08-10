// Aplica a máscara monetária aos campos de preço.
document.addEventListener("DOMContentLoaded", function () {
    const camposPreco = document.querySelectorAll(
        "[data-mascara-preco]"
    );

    camposPreco.forEach(function (campo) {
        campo.addEventListener("input", function () {
            const digitos = campo.value.replace(/\D/g, "");

            if (!digitos) {
                campo.value = "";
                return;
            }

            const valor = Number(digitos) / 100;

            campo.value = valor.toLocaleString("pt-BR", {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2
            });
        });
    });
});
