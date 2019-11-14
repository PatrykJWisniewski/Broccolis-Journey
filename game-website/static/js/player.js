$('document').ready(function(){

	//sign user out when sign out button is clicked
	$('#sign-out').on('click', function(){
		firebase.auth().signOut().then(function() {
		// Sign-out successful.
		}).catch(function(error) {
		  // An error happened.
		  console.log(error);
		});
	});

	//redirect to login page if user is not signed in
	firebase.auth().onAuthStateChanged(function(user) {
	  if (!user) {
	  	window.location = "../index.html";
	  }
	  else{
	  	$('#curUser').html(firebase.auth().currentUser.email);
	  	$('.profile-email').html(firebase.auth().currentUser.email);
	  	$('.profile-username').html(firebase.auth().currentUser.username);
	  }
	});

	$('#open-profile').on('click', function(){
		$('.userprofile').css('display', 'block');
		$('.userprofile-sidebar').css('width', '400px');
	});

	$('.userprofile').on('click', function(e){
		if (e.target.className == 'userprofile')
		{
			$('.userprofile').css('display', 'none');
			$('.userprofile-sidebar').css('width', '0');
		}
	});


	var database = firebase.firestore();
	var docRef = database.collection("livefeed");

	$("#chatmessage-button").on("click", function(){
		var message = document.getElementById('chatmessage-input').value;
		var date = new Date();
		console.log(message);
		database.collection("messages").add({
			"message": message,
			"date": date.getDate(),
			"user": firebase.auth().currentUser.email
		})
		.then(function(docRef){
			console.log("document written with ID: " + docRef.id);
		})
		.catch(function(error){
			console.log(error);
		});
	});

	$('#scoreboard-button').on('click', function(){
		console.log('clicked');
		$('#scoreboard-modal').modal('show');
	})

});
