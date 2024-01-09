function showToast(message, type) {
    var toastOptions = {
        text: message,
        position: 'top-right',
        loaderBg: type === 'error' ? '#f44336' : '#4CAF50',
        bgColor: type === 'error' ? '#D32F2F' : '#4CAF50',
        textColor: '#ffffff',
        hideAfter: 5000, // Hide the toast after 5 seconds (adjust as needed)
        stack: false, // Do not stack toasts
    };

    $.toast(toastOptions);
}
