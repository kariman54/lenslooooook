document.addEventListener('DOMContentLoaded', function () {
  const form = document.getElementById('glassesRepairForm');
  const successModal = document.getElementById('successModal');
  const closeModalBtn = document.querySelector('.close-btn');
  const customAlert = document.getElementById('customAlert');
  const closeAlertBtn = document.querySelector('.close-alert');

  form.addEventListener('submit', function (e) {
    e.preventDefault();

    const fullName = document.getElementById('fullName').value.trim();
    const contactNumber = document.getElementById('contactNumber').value.trim();
    const emailAddress = document.getElementById('emailAddress').value.trim();
    const homeAddress = document.getElementById('homeAddress').value.trim();
    const preferredDate = document.getElementById('preferredDate').value.trim();
    const repairReason = document.getElementById('repairReason').value.trim();

    if (!fullName || !contactNumber || !homeAddress || !preferredDate || !repairReason) {
      showAlert('Please fill out all required fields.');
      return;
    }

    if (!validatePhone(contactNumber)) {
      showAlert('Please enter a valid phone number.');
      return;
    }

    if (emailAddress && !validateEmail(emailAddress)) {
      showAlert('Please enter a valid email address.');
      return;
    }

    showModal();
  });

  closeModalBtn.addEventListener('click', function () {
    successModal.style.display = 'none';
  });

  closeAlertBtn.addEventListener('click', function () {
    customAlert.classList.remove('show');
  });

  function validatePhone(phone) {
    const phoneRegex = /^[0-9]{10,15}$/;
    return phoneRegex.test(phone);
  }

  function validateEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }

  function showAlert(message) {
    document.getElementById('alertMessage').textContent = message;
    customAlert.classList.add('show');
    setTimeout(() => {
      customAlert.classList.remove('show');
    }, 5000);
  }

  function showModal() {
    successModal.style.display = 'block';
  }
});
