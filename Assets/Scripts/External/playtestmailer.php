<?php
	require 'phpmailer/PHPMailerAutoload.php';

    $email = new PHPMailer();
	$email->From      = 'karlo@accidentalrebel.com';
	$email->FromName  = 'PlayTestMailer';
	$email->Subject   = $_POST['emailSubject'];
	$email->Body      = $_POST["emailTxt"];
	$email->AddAddress( $_POST["toEmail"] );
	
	$string 	= $_POST['fileData'];
	$filename 	= $_POST['fileName'];
	$encoding	= 'base64';
	$type 		= 'text/plain';
	
	$email->AddStringAttachment($string,$filename,$encoding,$type);
	
	$email->Send();
?>