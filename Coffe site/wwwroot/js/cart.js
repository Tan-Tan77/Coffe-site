

    const token = localStorage.getItem('token');

    let buttons = document.querySelectorAll('.dessert-item');



    buttons.forEach(function(button){
        button.addEventListener('click', async function (e) {



            e.preventDefault();
            let item = e.target.id;

            const btn = document.getElementById(item);

            if (btn.classList.contains('buttons-in-cart') == false) {

                btn.textContent = 'В корзине';
                btn.classList.remove('buttons');
                btn.classList.add('buttons-in-cart');
                btn.classList.add('btn-padding');

                if (localStorage.getItem('token') == null) {


                    const response = await fetch('/api/cart/add-to-cart', {

                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({ newphone: item })

                    });

                    const result = await response.json();

                    if (localStorage.getItem('cart') != null) {

                        const arrToAdd = JSON.parse(localStorage.getItem('cart'));

                        arrToAdd.push(result);
                        localStorage.setItem('cart', JSON.stringify(arrToAdd));



                        console.log(result);
                    } else {

                        const arrayY = [];

                        arrayY.push(result);

                        localStorage.setItem('cart', JSON.stringify(arrayY));
                    }

                } else {



                    const response2 = await fetch('/api/cart/add-to-cart-auth', {

                        method: 'POST',
                        headers: { 'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json' },
                        body: JSON.stringify({ newphone: item })

                    });

                    const result2 = await response2.json();

                    console.log(result2);

                }
            } else {

                if (localStorage.getItem('token') != null) {

                    btn.textContent = 'В корзину';
                    btn.classList.add('buttons');
                    btn.classList.remove('buttons-in-cart');
                    btn.classList.remove('btn-padding');

                    const responseToDelete = await fetch('/api/cart/delete', {

                        method: 'POST',
                        headers: { 'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json' },
                        body: JSON.stringify(item)

                    });
                    const resultDelete = await responseToDelete.json();

                    console.log(item);
                    console.log(resultDelete.success);

                    if (resultDelete.success) {
                        console.log(resultDelete);

                    } else {

                        console.log(resultDelete);
                    }
                } else {



                    btn.textContent = 'В корзину';
                    btn.classList.add('buttons');
                    btn.classList.remove('buttons-in-cart');
                    btn.classList.remove('btn-padding');




                    if (localStorage.getItem('cart') != null) {

                        if (btn.classList.contains('buttons-in-cart') == false) {



                            const arr = JSON.parse(localStorage.getItem('cart'));
                            const newCart = [];
                            console.log(arr, 'newCart');

                            for (let element of arr) {



                                if (element.product_Id == item) {

                                    console.log('not pushed', element);

                                } else {
                                    newCart.push(element);
                                }

                            }

                            localStorage.removeItem('cart');
                            console.log(newCart, 'newCart');
                            localStorage.setItem('cart', JSON.stringify(newCart));

                        }


                    }
                }

            }


        });
});


    window.addEventListener('load', async function(){

    
            if(token != null){

            const getItemsFromDb = await fetch('/api/cart/cart-from-db',{

        method:'POST',
    headers:{'Authorization':`Bearer ${token}`, 'Content-Type':'application/json'}

            });

    const cart = await getItemsFromDb.json();

            if(cart.length > 0){


            for (let itemOfCart of cart){

                try {

        console.log(itemOfCart.product_Id);
    let element = document.getElementById(itemOfCart.product_Id);
    element.textContent = 'В корзине';
    element.classList.remove('buttons');
    element.classList.add('buttons-in-cart');
                    element.classList.add('btn-padding');

                } catch (err) {

                }
            }

            }else{
        console.log('cart is empty');
                
            }
            }else{

                    if(localStorage.getItem('cart') != null){


        let arrOfCart = JSON.parse(localStorage.getItem('cart'));

       for (let arrItem of arrOfCart) {

           try {

               let btnToChange = document.getElementById(arrItem.product_Id);
               btnToChange.textContent = "В корзине";
               btnToChange.classList.remove('buttons');
               btnToChange.classList.add('buttons-in-cart');
               btnToChange.classList.add('btn-padding');
           }catch (err) {
               
           }
                       }
                       }
            }

});



