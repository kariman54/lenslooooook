document.getElementById('appointmentFormOnline').addEventListener('submit', function(event) {
  // Get form elements
  const name = document.getElementById('name');
  const phone = document.getElementById('phone');
  const email = document.getElementById('email');
  const date = document.getElementById('date');
  const service = document.getElementById('service');
  
  // Regular expression patterns
  const phonePattern = /^\d{11}$/; // Simple pattern for 11 digit phone numbers
  const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Simple email pattern

  // Validation checks
  if (name.value.trim() === '') {
    showAlert('Full Name is required');
    name.focus();
    event.preventDefault();
    return false;
  }

  if (!phonePattern.test(phone.value.trim())) {
    showAlert('Please enter a valid 11 digit phone number');
    phone.focus();
    event.preventDefault();
    return false;
  }

  if (!emailPattern.test(email.value.trim())) {
    showAlert('Please enter a valid email address');
    email.focus();
    event.preventDefault();
    return false;
  }

  if (date.value.trim() === '') {
    showAlert('Preferred Date is required');
    date.focus();
    event.preventDefault();
    return false;
  }

  if (service.value.trim() === '') {
    showAlert('Please select a service');
    service.focus();
    event.preventDefault();
    return false;
  }

  // If all checks pass, show success modal (if applicable)
  event.preventDefault();
  const modal = document.getElementById('customModal');
  modal.style.display = 'block';

  // Close the modal when the user clicks the close button or outside the modal
  const closeBtn = document.querySelector('.close-btn');
  closeBtn.onclick = function() {
    modal.style.display = 'none';
  }

  window.onclick = function(event) {
    if (event.target == modal) {
      modal.style.display = 'none';
    }
  }
});

// Function to show custom alert
function showAlert(message) {
  const alertBox = document.getElementById('customAlert');
  const alertMessage = document.getElementById('alertMessage');
  alertMessage.textContent = message;
  alertBox.style.display = 'block';

  // Close alert on click
  const closeAlert = document.querySelector('.close-alert');
  closeAlert.onclick = function() {
    alertBox.style.display = 'none';
  }

  // Automatically hide alert after 3 seconds
  setTimeout(() => {
    alertBox.style.display = 'none';
  }, 3000);
}