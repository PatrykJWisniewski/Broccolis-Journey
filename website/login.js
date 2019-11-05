$('document').ready(function(){

	//make login box slide in from right
	$('.body-right').css("transform", "translateX(0)");
	$('.body-container').css("backgroundColor", "rgba(10, 10, 10, 0.7)");

	$('.signup-container').css('transform', 'scale(0)');
	$('.signup-container').css('height', '0');
	$('.login-container').css('transform', 'scale(1)');
	$('.login-container').css('height', '100%');

	//toggle between login and sign up
	$('.signup-link').on('click', function(){
		$('.login-container').css('height', '0');
		$('.signup-container').css('transform', 'scale(1)');
		$('.signup-container').css('height', '100%');
		$('.login-container').css('transform', 'scale(0)');
		
	});

	$('.login-link').on('click', function(){
		$('.signup-container').css('transform', 'scale(0)');
		$('.signup-container').css('height', '0');
		$('.login-container').css('transform', 'scale(1)');
		$('.login-container').css('height', '100%');
	});

	//create new user in database when sign up button is clicked
	$('#signup-button').on('click', function(){
		$('.signup-error-message').empty();
		let email = $('.signup-container #signup-username').val();
		let password;

		//check if passwords match
		if ($('#signup-password').val() === $('#signup-confirmpassword').val()){
			password = $('#signup-password').val();
		}
		else{
			$('.signup-error-message').append('<div>Passwords do not match</div>');
			return;
		}

		firebase.auth().createUserWithEmailAndPassword(email, password).catch(function(error) {
			if (error.code == 'auth/invalid-email'){
				$('.signup-error-message').append('<div>Please enter a valid email</div>');
			}
			if (error.code == 'auth/email-already-in-use'){
				$('.signup-error-message').append('<div>This email is already in use</div>');
			}
			if (error.code == 'auth/weak-password'){
				$('.signup-error-message').append('<div>Password must be at least 6 characters</div>');
			}
		  console.log(error);
		});

	});

	//log in when login button is clicked then redirect to game page
	$('#login-button').on('click', function(){
		$('.login-error-message').empty();
		let email = $('#login-username').val();
		let password = $('#login-password').val();
		firebase.auth().signInWithEmailAndPassword(email, password).catch(function(error) {
		  	if (error.code == 'auth/wrong-password'){
				$('.login-error-message').append('<div>Incorrect password</div>');
			}
			if (error.code == 'auth/user-not-found'){
				$('.login-error-message').append('<div>Email not found</div>');
			}
			if (error.code == 'auth/invalid-email'){
				$('.login-error-message').append('<div>Invalid Email</div>');
			}
			console.log(error);

		});

	});

	//redirect on signup/login
	firebase.auth().onAuthStateChanged(function(user) {
	  if (user) {
	  	window.location = "webGL-3/index.html";
	  }
	});

});