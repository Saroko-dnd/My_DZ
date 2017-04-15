package com.example.admin.firstapplication;

import android.app.Activity;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class SecondActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_second);
        String messageFromMainActivity = getIntent().getStringExtra("messageFromMainActivity");
        TextView TextViewInThisActivity = (TextView)findViewById(R.id.secondActivityTextView);
        TextViewInThisActivity.setText(messageFromMainActivity);

        Button returnResultbutton = (Button) findViewById(R.id.createResultButton);
        returnResultbutton.setOnClickListener(new Button.OnClickListener() {
            public void onClick(View v) {
                EditText messageEditor = (EditText)findViewById(R.id.editorSecondActivity);
                Intent returnIntent = new Intent();
                returnIntent.putExtra("result",messageEditor.getText().toString());
                setResult(Activity.RESULT_OK,returnIntent);
                finish();
            }
        });
    }
}
