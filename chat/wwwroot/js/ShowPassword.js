
function toggleShowPassword() {
	var passwordTextBox = document.getElementById('passwordTextBox');
	if (passwordTextBox.getAttribute('type') == 'text') {
		passwordTextBox.type = 'password';
	}
	else {
		passwordTextBox.type = 'text';
	}
}