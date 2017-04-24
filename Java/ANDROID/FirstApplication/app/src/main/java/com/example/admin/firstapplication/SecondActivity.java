package com.example.admin.firstapplication;

import android.app.Activity;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Bitmap;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;

import java.io.File;

public class SecondActivity extends AppCompatActivity {

    private static int I_WANT_PHOTO = 1;
    SQLiteDatabase testDB;

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

        //Creation of database and insertion
        File database = getApplicationContext().getDatabasePath("users_test");
        if (!database.exists()) {
            // Database does not exist so copy it from assets here
            testDB = openOrCreateDatabase("users_test",MODE_PRIVATE,null);
            testDB.execSQL("CREATE TABLE IF NOT EXISTS users_test_info(Username VARCHAR,Password VARCHAR, Age INTEGER);");
            /*testDB.execSQL("INSERT INTO users_test_info VALUES('Super user','gggttt444', 28);");
            testDB.execSQL("INSERT INTO users_test_info VALUES('Big user','123ty', 45);");
            testDB.execSQL("INSERT INTO users_test_info VALUES('Small user','789hj', 19);");*/
        } else {
            testDB = openOrCreateDatabase("users_test",MODE_PRIVATE,null);
        }
        //Retrieving data from db
        Cursor resultSet = testDB.rawQuery("Select * from users_test_info",null);
        resultSet.moveToFirst();
        TextView TextViewForDbTest = (TextView)findViewById(R.id.secondActivityForDbTest);
        String textFromDB = "";
        do{
            textFromDB += "Username - " + resultSet.getString(0) + " Password - " + resultSet.getString(1)
                    + " Age - " + resultSet.getString(2) + "\n";
        }while(resultSet.moveToNext());
        TextViewInThisActivity.setText(String.valueOf(resultSet.getCount()));
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.second_activity_menu, menu);//Menu Resource, Menu
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        TextView TextViewInThisActivity = (TextView)findViewById(R.id.secondActivityTextView);
        switch (item.getItemId()) {
            case R.id.item1:
                TextViewInThisActivity.setText("selected item 1");
                return true;
            case R.id.item2:
                TextViewInThisActivity.setText("selected item 2");
                return true;
            case R.id.item3:
                TextViewInThisActivity.setText("selected item 3");
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == I_WANT_PHOTO && resultCode == Activity.RESULT_OK) {
            ImageView imageViewForPhoto = (ImageView)findViewById(R.id.imageViewForCameraPhoto);
            imageViewForPhoto.setImageBitmap((Bitmap)data.getExtras().get("data"));
        }
    }
}
