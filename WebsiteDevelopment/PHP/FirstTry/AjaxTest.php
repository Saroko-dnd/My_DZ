<?php
    $StringFromUser = NULL;
    $StringFromUser = $_REQUEST["UserString"];
    if ($StringFromUser != NULL)
    {
        echo strlen($StringFromUser);
    }
?>