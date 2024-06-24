// forms js
//  to open the form of create new password 
function openNewPasswordPopup() {
    $('#newPasswordPopup').modal('show');
};



// validation of the login popup

document.addEventListener('DOMContentLoaded', (event) => {
    const loginForm = document.querySelector('.login_form');
    const usernameInput = document.getElementById('log_username');
    const passwordInput = document.getElementById('log_password');
    const rememberMeInput = document.getElementById('rememberMe');

    loginForm.addEventListener('submit', function (event) {
        // Remove existing error messages
        const errorMessages = document.querySelectorAll('.error-message');
        errorMessages.forEach(msg => msg.remove());

        let isValid = true;

        if (usernameInput.value.trim() === '') {
            showError(usernameInput, 'Username is required');
            isValid = false;
        }

        if (passwordInput.value.trim() === '') {
            showError(passwordInput, 'Password is required');
            isValid = false;
        }

        // If the form is not valid, prevent submission
        if (!isValid) {
            event.preventDefault();
        }
    });

    function showError(input, message) {
        const errorElement = document.createElement('div');
        errorElement.classList.add('error-message');
        errorElement.style.color = 'red';
        errorElement.innerText = message;
        input.parentElement.appendChild(errorElement);
    }
});



// validation for the forgotpassword popup 

document.addEventListener("DOMContentLoaded", function () {
    const forgotPasswordForm = document.getElementById('forgotPasswordForm');

    forgotPasswordForm.addEventListener('submit', function (event) {
        event.preventDefault(); // Prevent default form submission

        // Get input value
        const email = document.getElementById('forgot_email').value.trim();

        // Validate input field
        if (email === '') {
            alert('Please enter your email address.');
            return;
        }

        // If validation passes, submit the form
        this.submit();
    });
});




// validation for new password form  

document.addEventListener("DOMContentLoaded", function () {
    const newPasswordForm = document.getElementById('newPasswordForm');

    newPasswordForm.addEventListener('submit', function (event) {
        event.preventDefault(); // Prevent default form submission

        // Get input values
        const newPassword = document.getElementById('new_password').value.trim();
        const confirmPassword = document.getElementById('confirm_password').value.trim();

        // Validate input fields
        if (newPassword === '') {
            alert('Please enter your new password.');
            return;
        }

        if (confirmPassword === '') {
            alert('Please confirm your new password.');
            return;
        }

        if (newPassword !== confirmPassword) {
            alert('Passwords do not match. Please try again.');
            return;
        }

        // If validation passes, submit the form
        this.submit();
    });
});



// // validation for the sign up fprm

document.addEventListener('DOMContentLoaded', (event) => {
    const registrationForm = document.getElementById('registrationForm');
    const firstNameInput = document.getElementById('reg_firstName');
    const lastNameInput = document.getElementById('reg_lastName');
    const emailInput = document.getElementById('reg_email');
    const dateOfBirthInput = document.getElementById('dateOfBirth');
    const genderInputs = document.getElementsByName('gender');
    const passwordInput = document.getElementById('reg_password');
    const confirmPasswordInput = document.getElementById('confirmPassword');
    const termsAndConditionsInput = document.getElementById('termsAndConditions');
    const errorAlert = document.getElementById('errorAlert');
    const errorMessageDiv = errorAlert.querySelector('.error-message');

    registrationForm.addEventListener('submit', function (event) {
        // Clear previous error messages
        errorMessageDiv.innerHTML = '';
        errorAlert.style.display = 'none';

        let isValid = true;
        let messages = [];

        if (firstNameInput.value.trim() === '') {
            messages.push('First Name is required');
            isValid = false;
        }

        if (lastNameInput.value.trim() === '') {
            messages.push('Last Name is required');
            isValid = false;
        }

        if (emailInput.value.trim() === '') {
            messages.push('Email address is required');
            isValid = false;
        }

        if (dateOfBirthInput.value.trim() === '') {
            messages.push('Date of Birth is required');
            isValid = false;
        }

        let genderSelected = false;
        genderInputs.forEach((input) => {
            if (input.checked) {
                genderSelected = true;
            }
        });

        if (!genderSelected) {
            messages.push('Gender is required');
            isValid = false;
        }

        if (!validatePassword(passwordInput.value)) {
            messages.push('Password must be at least 8 characters long, including uppercase, lowercase, numbers, and symbols.');
            isValid = false;
        }

        if (passwordInput.value !== confirmPasswordInput.value) {
            messages.push('Passwords do not match');
            isValid = false;
        }

        if (!termsAndConditionsInput.checked) {
            messages.push('You must agree to the Terms and Conditions');
            isValid = false;
        }

        // If the form is not valid, prevent submission and show error messages
        if (!isValid) {
            event.preventDefault();
            messages.forEach((message) => {
                const messageElement = document.createElement('div');
                messageElement.textContent = message;
                errorMessageDiv.appendChild(messageElement);
            });
            errorAlert.style.display = 'block';
        }
    });

    function validatePassword(password) {
        const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
        return regex.test(password);
    }

    // Close the error alert
    document.querySelector('.close-btn').addEventListener('click', () => {
        errorAlert.style.display = 'none';
    });
});




// first home section

$(document).ready(function () {
    $('#imageCarousel').carousel({
        interval: 2000 // 2 seconds
    });

    $('#imageCarousel').on('slide.bs.carousel', function () {
        $('.carousel-caption').removeClass('animated').css('transform', 'translateX(-100%)');
    });

    $('#imageCarousel').on('slid.bs.carousel', function () {
        $('.carousel-caption').addClass('animated').css('transform', 'translateX(0)');
    });
});



// validation contact Us

document.getElementById('contactForm').addEventListener('submit', function (event) {
    event.preventDefault();
    let isValid = true;
    let errorMessage = '';

    const name = document.getElementById('name').value.trim();
    const phone = document.getElementById('phone').value.trim();
    const email = document.getElementById('email').value.trim();
    const message = document.getElementById('message').value.trim();

    // Regular expressions for validation
    const phonePattern = /^[0-9]{11}$/;
    const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;

    if (name === '') {
        isValid = false;
        errorMessage += 'Full Name is required.<br>';
    }
    if (!phonePattern.test(phone)) {
        isValid = false;
        errorMessage += 'Phone Number must be 11 digits.<br>';
    }
    if (!emailPattern.test(email)) {
        isValid = false;
        errorMessage += 'Email Address is invalid.<br>';
    }
    if (message === '') {
        isValid = false;
        errorMessage += 'Message is required.<br>';
    }

    if (isValid) {
        // Simulate form submission success
        document.getElementById('contactForm').reset();
        document.getElementById('alert-success').classList.remove('d-none');
        setTimeout(function () {
            document.getElementById('alert-success').classList.add('d-none');
        }, 3000);
    } else {
        // Display error message
        const errorAlert = document.getElementById('errorAlert');
        errorAlert.style.display = 'block';
        errorAlert.querySelector('.error-message').innerHTML = errorMessage;
    }
});

document.querySelector('.close-btn').addEventListener('click', function () {
    document.getElementById('errorAlert').style.display = 'none';
});


