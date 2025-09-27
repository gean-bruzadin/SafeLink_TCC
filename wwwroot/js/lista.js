const items = document.querySelectorAll('.complaint-item');
const detail = document.getElementById('detail-template');
let activeItem = null;

// abre o detalhe ao clicar na denúncia
items.forEach(item => {
    item.addEventListener('click', () => {
        if (activeItem === item) return; // já aberto nesse item
        item.insertAdjacentElement("afterend", detail);
        detail.style.display = "block";
        activeItem = item;

        const id = item.getAttribute("data-id");
        document.getElementById("detail-description").textContent = 
            `Detalhes completos da denúncia ${id}`;
    });
});

// botão voltar
document.getElementById('back-button').addEventListener('click', () => {
    detail.style.display = "none";
    activeItem = null;
    console.log(error);
});