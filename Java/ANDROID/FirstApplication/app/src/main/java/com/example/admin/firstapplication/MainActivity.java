package com.example.admin.firstapplication;

import android.app.Activity;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    static final int GET_NAME_SECOND_NAME_REQUEST = 1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Button startSecondActivityButton = (Button) findViewById(R.id.StartSecondActivityButton);
        startSecondActivityButton.setOnClickListener(new Button.OnClickListener() {
            public void onClick(View v) {
                EditText messageEditor = (EditText)findViewById(R.id.editorMessageForSecondActivity);
                Intent intent = new Intent(getBaseContext(), SecondActivity.class);
                intent.putExtra("messageFromMainActivity", messageEditor.getText().toString());
                //startActivity(intent);
                startActivityForResult(intent, GET_NAME_SECOND_NAME_REQUEST);
            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == GET_NAME_SECOND_NAME_REQUEST) {
            if(resultCode == Activity.RESULT_OK){
                String result=data.getStringExtra("result");
                TextView textViewForHelloWorld = (TextView)findViewById(R.id.HelloWorldTextView);
                textViewForHelloWorld.setText(result);
            }
            if (resultCode == Activity.RESULT_CANCELED) {
                TextView textViewForHelloWorld = (TextView)findViewById(R.id.HelloWorldTextView);
                textViewForHelloWorld.setText("RESULT CANCELED");
            }
        }
    }
}
