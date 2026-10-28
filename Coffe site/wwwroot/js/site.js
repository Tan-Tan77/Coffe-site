
let _textv = " ";
let _textLogin = " ";






$(document).ready(function () {


    //const TOKEN = "b6b3ef64d95d6ef80c6c18be5c0c75c8c6a8508a";

    //function msk() {
    //    $("#address").suggestions({
    //        token: TOKEN,
    //        type: "ADDRESS",
    //        constraints: {
             
    //            locations: { region: "Москва" },
    //        },
          
    //        restrict_value: true
    //    });
    //}



    
    jQuery(function ($) {
        $('#phone-reg').mask("+79999999999", { placeholder: "" });
        
    });
   
    jQuery(function ($) {
        $('#phone-login').mask("+79999999999", { placeholder: "" });
    });


    $('#ModalForRegistration').on('show.bs.modal', function () {
        $('#myModal').modal('hide');
        $('#phone-login').val('');
        $('#password-login').val('');
       
        _textv = " "; 
        document.getElementById('validateText').textContent = _textv;

        

    });



    $('#myModal').on('show.bs.modal', function () {

        $('#phone-login').val('');
        $('#password-login').val('');
        $('#ModalForRegistration').modal('hide');
        $('#phone-reg').val('');
        $('#password-reg').val('');
        _textLogin = "";
        document.getElementById('validateTextLogin').textContent = _textLogin;


    });

  
});





function EnteredLogin(){

    if (localStorage.getItem('token') != null) {
        _textv = "Вы уже вошли в аккаунт";
        document.getElementById('validateText').textContent = _textv;
        return false
    }
    document.getElementById('validateText').textContent = _textv;
    return true
}

function EnteredLogin2() {

    if (localStorage.getItem('token') != null) {
        _textLogin = "Вы уже вошли в аккаунт";
        document.getElementById('validateTextLogin').textContent = _textLogin;
        return false
    }
    document.getElementById('validateTextLogin').textContent = _textLogin;
    return true
}




function AllNull(phon, pass) {


    if (phon == '' & pass == '') {

        _textv = "Введите номер и пароль";
        document.getElementById('validateText').textContent = _textv;
        return false
    }
    document.getElementById('validateText').textContent = _textv;
    return true
}





function NullPnoneReg(phoneVal) {

    
    if (phoneVal === '') {

         _textv = "Введите номер телефона";
      
        return false
    } 
    document.getElementById('validateText').textContent = _textv;
    return true

}

function NullPasswordReg(passwordVal) {

    if (passwordVal === '') {

       
        _textv = "Введите пароль";
      

        return false
    } 
    document.getElementById('validateText').textContent = _textv;
    
    return true

}


function PasswordLenght(passwordVal) {

   

    if (passwordVal.length < 6) {

       
        _textv = "Минимальная длина пароля - 6 символов";
     
        return false
    } 
        document.getElementById('validateText').textContent = _textv;
    
    return true
}
















function AllNull2(phon, pass) {


    if (phon == '' & pass == '') {

        _textLogin = "Введите номер и пароль";
        document.getElementById('validateTextLogin').textContent = _textLogin;
        return false
    }
    document.getElementById('validateTextLogin').textContent = _textLogin;
    return true
}





function NullPnoneReg2(phoneVal) {


    if (phoneVal === '') {

        _textLogin = "Введите номер телефона";

        return false
    }
    document.getElementById('validateTextLogin').textContent = _textLogin;
    return true

}

function NullPasswordReg2(passwordVal) {

    if (passwordVal === '') {


        _textLogin = "Введите пароль";


        return false
    }
    document.getElementById('validateTextLogin').textContent = _textLogin;

    return true

}


function PasswordLenght2(passwordVal) {



    if (passwordVal.length < 6) {


        _textLogin = "Минимальная длина пароля - 6 символов";

        return false
    }
    document.getElementById('validateTextLogin').textContent = _textLogin;

    return true
}


async function LoadCartToDb() {

    if (localStorage.getItem('cart') != null) {

        var arr = JSON.parse(localStorage.getItem('cart'));

        console.log('loading started');

        const _token = localStorage.getItem('token');


        for (let item of arr) {
            console.log(item);

            console.log(arr);



            const responseToCartDb = await fetch('/api/cart/add-to-db-from-cookie', {


                method: 'POST',
                headers: { 'Authorization': `Bearer ${_token}`, 'Content-Type': 'application/json' },
                body: JSON.stringify(item)

            });

            const resultOfDb = await responseToCartDb.json();

            if (resultOfDb.success) {

                localStorage.removeItem('cart');
                console.log('loaded to db');
                console.log(item);
              

            } else {
                console.log('didnt load to db');
                console.log(item);
               
            }
        }
        localStorage.removeItem('cart');
        window.location.href = '/Home/Account';
    }



}






document.addEventListener('DOMContentLoaded', function () {
    const registerBtn = document.querySelector('#ModalForRegistration button[type="submit"]');

    const loginBtn = document.querySelector('#myModal button[type="submit"]');


    if (registerBtn) {
        registerBtn.addEventListener('click', async function (e) {
            e.preventDefault();
          
            const phone = document.getElementById('phone-reg').value;
            const password = document.getElementById('password-reg').value;

          


            if (EnteredLogin() == true & NullPasswordReg(password) == true & NullPnoneReg(phone) == true & PasswordLenght(password) == true & AllNull(phone, password) == true) {



              
                const response = await fetch('/api/auth/register', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        phone: phone,
                        password: password
                    })
                });




                const result = await response.json();

                if (result.success == true) {


                    localStorage.setItem('token', result.token);

                    console.log('successesful registration');

                    LoadCartToDb();


                } else {

                    document.getElementById('validateText').textContent = result.message;

                    console.log('Результат завпроса: ', result);
                    console.log('Запрос: ', response);
                }
            } else {

                console.log('Валидация не пройдена');

            }

        })
    
    }


    if (loginBtn) {


        loginBtn.addEventListener('click', async function (e){

            e.preventDefault();

            const loginPhone = document.getElementById('phone-login').value;

            const loginPass = document.getElementById('password-login').value;

            if (EnteredLogin2() == true & NullPasswordReg2(loginPass) == true & NullPnoneReg2(loginPhone) == true & PasswordLenght2(loginPass) == true & AllNull2(loginPhone, loginPass) == true  ) {




                const response = await fetch('/api/auth/login',

                    {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },

                        body: JSON.stringify(

                            {
                                phone: loginPhone,
                                password: loginPass

                            })
                    });


                const result = await response.json();

                if (result.success == true) {

                    localStorage.setItem('token', result.token);
            

                    console.log('successesful enter');

                    LoadCartToDb();


                }else {

                    document.getElementById('validateTextLogin').textContent = result.message;
                    console.log('not entered');
                }


            }
            else {
                console.log('validation error');
            }
        });

    }




})