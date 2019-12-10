// /public/javascript.js


//TODO: fix autoscroll
//TODO: add currentUser functionality :LINE 77
//TODO: Implement security'
//Organize firestore data
// function addChut(user_id) {
//     var log= document.createElement('div');
//     log.className= 'log';
//     var textarea= document.createElement('textarea');
//     var input= document.createElement('input');
//     input.value= user_id;
//     input.readonly= True;
//     var button= document.createElement('input');
//     button.type= 'button';
//     button.value= 'Message';
//
//     var chut= document.createElement('div');
//     chut.className= 'chut';
//     chut.appendChild(log);
//     chut.appendChild(textarea);
//     chut.appendChild(input);
//     chut.appendChild(button);
//     document.getElementById('chuts').appendChild(chut);
//
//     button.onclick= function() {
//         alert('Send '+textarea.value+' to '+user_id);
//     };
//
//     return chut;
// }

//https://stackoverflow.com/questions/2794137/sanitizing-user-input-before-adding-it-to-the-dom-in-javascript

window.alert = function() {};

function isAlphaNumeric(str) {
  var code, i, len;

  for (i = 0, len = str.length; i < len; i++) {
    code = str.charCodeAt(i);
    if (!(code > 47 && code < 58) && // numeric (0-9)
        !(code > 64 && code < 91) && // upper alpha (A-Z)
        !(code > 96 && code < 123) &&  // lower alpha (a-z)
        !(code == 32 || code == 33 || code == 44 || code == 46 || code == 63))// space, exclamation point, comma, period,  and question mark
        {return false;}
  }
  return true;
};



//To retrieve date
function getDate(){
  var today = new Date();
  return today;
}


  var database = firebase.firestore();
  var docRef = database.collection("messages");


  // When the form is submitted send message to firebase
  $(document).ready(function() {
    $('#chatmessage-button').click(function () {
        // Retrieve the message from the user
        var message = $('#chatmessage-input').val();
        //Don't take unsafe inputs
        if(!isAlphaNumeric(message)){
          alert("Please use only Alpha-numeric characters and . , ! ?")
        }else{
          //Send the message to the server
          docRef.add({
            // "user": "me",
            "user": firebase.auth().currentUser.displayName, //maybe change to username
            "message": message,
            "date": getDate(),
            "photoURL": firebase.auth().currentUser.photoURL
          })
          .then(function(docRef){
        console.log("document written with ID: " + docRef.id);
        })
        .catch(function(error){
        console.log(error);
        });
        }
      // Clear the input and focus it for a new message
      $('#chatmessage-input').val("");
      $('#chatmessage-input').focus();
      // $(e.target).find('input').focus();

      $(".live-feed").animate({ scrollTop: 100000000000 }, "slow");
    });

  });