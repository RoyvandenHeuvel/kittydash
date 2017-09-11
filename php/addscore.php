<?php 
		$db = new PDO('mysql:host=localhost;dbname=kittydash;charset=utf8', 'root', 'kittydash');

        $name = $_GET['name']; 
        $score = $_GET['score']; 
        $hash = $_GET['hash']; 
 
        $secretKey="kitty_dash_highscore_key"; # Change this value to match the value stored in the client javascript below 

        $real_hash = md5($name . $score . $secretKey); 
        if($real_hash == $hash) { 
            // Send variables for the MySQL database class. 
            $db->exec("insert into scores values (NULL, '$name', '$score');"); 
        } 
?>