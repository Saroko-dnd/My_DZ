package com.example.admin.firstapplication;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;

public class SecondActivity extends AppCompatActivity {

    private static int I_WANT_PHOTO = 1;

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

        Button runCameraButton = (Button) findViewById(R.id.runCameraButton);
        runCameraButton.setOnClickListener(new Button.OnClickListener() {
            public void onClick(View v) {
                Intent runCameraIntent = new Intent(android.provider.MediaStore.ACTION_IMAGE_CAPTURE);
                startActivityForResult(runCameraIntent,I_WANT_PHOTO);
            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == I_WANT_PHOTO && resultCode == Activity.RESULT_OK) {
            ImageView imageViewForPhoto = (ImageView)findViewById(R.id.imageViewForCameraPhoto);
            imageViewForPhoto.setImageBitmap((Bitmap)data.getExtras().get("data"));
        }
    }
}
