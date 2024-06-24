document.addEventListener('DOMContentLoaded', function () {
  document.getElementById('appointmentFormHome').addEventListener('submit', function (event) {
    let form = event.target;
    let isValid = true;
    let messages = [];

    // Full Name Validation
    let name = form.name.value.trim();
    if (name === "") {
      isValid = false;
      messages.push("Full Name is required.");
    }

    // Phone Number Validation
    let phone = form.phone.value.trim();
    let phonePattern = /^\d{11}$/;
    if (phone === "") {
      isValid = false;
      messages.push("Phone Number is required.");
    } else if (!phonePattern.test(phone)) {
      isValid = false;
      messages.push("Phone Number must be 11 digits.");
    }

    // Email Address Validation (optional but if provided, must be valid)
    let email = form.email.value.trim();
    let emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (email !== "" && !emailPattern.test(email)) {
      isValid = false;
      messages.push("Email Address is not valid.");
    }

    // Home Address Validation
    let address = form.address.value.trim();
    if (address === "") {
      isValid = false;
      messages.push("Home Address is required.");
    }

    // Preferred Date Validation
    let date = form.date.value.trim();
    if (date === "") {
      isValid = false;
      messages.push("Preferred Date is required.");
    }

    // Reason for Visit Validation
    let reason = form.reason.value.trim();
    if (reason === "") {
      isValid = false;
      messages.push("Reason for Visit is required.");
    }

    if (!isValid) {
      event.preventDefault();
      alert(messages.join("\n"));
    }
  });
});