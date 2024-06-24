let iconCart = document.querySelector(".iconCart");
let cart = document.querySelector(".cart");
let container = document.querySelector("container");
let close = document.querySelector(".close");

iconCart.addEventListener("click", () => {
  if (cart.style.right == "-100%") {
    cart.style.right = "0";
    container.style.transform = "translateX(-400PX)";
  } else {
    cart.style.right = "-100%";
    container.style.transform = "translateX(0)";
  }
});

close.addEventListener("click", () => {
  cart.style.right = "-100%";
  container.style.transform = "translateX(0)";
});

let products = null;
// get data from file json

fetch("product.json")
  .then((response) => response.json())
  .then((data) => {
    products = data;
    addDataToHTML();
  });

// show datas in list html
function addDataToHTML() {
  //remove datas default in html
  let listProductHTML = document.querySelector(".listProduct");
  listProductHTML.innerHTML = "";
}


// add new datas 

