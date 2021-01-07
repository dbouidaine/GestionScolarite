
	function call_enseignant()
	{
  		document.getElementById('Enseignant_login').style.display="block";
  		document.getElementById('choose').style.display="none";
	}
	function call_etudiant()
	{
  		document.getElementById('Etudiant_login').style.display="block";
  		document.getElementById('choose').style.display="none";
	}
	function back()
	{

		document.getElementById('choose').style.display='block';
		document.getElementById('Enseignant_login').style.display="none";
		document.getElementById('Etudiant_login').style.display="none";
	}