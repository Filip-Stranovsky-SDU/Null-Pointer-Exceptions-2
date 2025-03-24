### Test Case 1: Login Screen
#### Objective: Verify that the login screen correctly handles user input and role-based authentication.

#### Steps:

- Launch the application.

- Enter a valid login and password for a student and click "Login".
 
- **Result:** Successfully logged in as Student. Redirected to the "Student Dashboard".

- Enter a valid login and password for a teacher and click "Login".

- **Result:** Successfully logged in as Teacher. Redirected to the "Teacher Dashboard".

- Enter invalid login credentials.

- **Result:** Error message displayed: "Invalid username or password".

### Test Case 2: Student – View Available Subjects

#### Objective: Verify that a student can view a list of available subjects.

#### Steps:

- Log in as a student.

- Navigate to the "Enroll" page.

- **Result:** List of available subjects is displayed.

- Subjects Displayed:

    - Mathematics (Description: An introduction to algebra and calculus., Teacher: raklo)


- Ensure that the list includes subject names, descriptions, and teacher details.

- **Result:** All subjects correctly displayed with accurate details.

### Test Case 3: Student – View Enrolled Subjects
#### Objective: Verify that a student can view the list of subjects they are enrolled in.

#### Steps:

- Log in as a student.

- Navigate to the "My Subjects" page.

- **Result:** List of enrolled subjects is displayed.

- Subjects Displayed:

    - Physics (Description: Fundamentals of mechanincs and thermodynamics, Teacher: fako)
    - Computer Science (Description: Programming basics and data structures, Teacher: raklo)

- Ensure that the enrolled subjects are fetched correctly from the saved data (JSON).

- **Result:** Subjects match the data from the JSON file.

### Test Case 5: Student – Enroll in Available Subjects
#### Objective: Verify that a student can enroll in available subjects.

#### Steps:

- Log in as a student.

- Navigate to the "Enroll" page.

- Select a subject (e.g., Mathematics) and click the "Enroll" button.

- **Result:** Subject "Mathematics" is added to the "My Subjects" list.

- Ensure that the enrollment data is saved in the JSON file.

- **Result:** Enrollment is saved correctly in the JSON file.

#### Test Case 6: Student – Drop From Enrolled Subjects
### Objective: Verify that a student can drop a subject from their enrolled subjects list.

### Steps:

- Log in as a student.

- Navigate to the "My Subjects" page.

- Select a subject (e.g., Mathematics) and click the "Drop" button.

- **Result:** Subject "Mathematics" is removed from the "Enrolled Subjects" list.

- Ensure that the removal is reflected in the JSON data.

- **Result:** Subject removal is saved correctly in the JSON file.

### Test Case 7: Teacher – View Subjects They Teach
#### Objective: Verify that a teacher can view the subjects they are teaching.
Teacher can indeed view their subjects

### Test Case 8: Teacher – Create a New Subject
#### Objective: Verify that a teacher can create a new subject.
Teacher can indeed create a new subject

### Test Case 9: Teacher – Edit Subject Details
#### Objective: Verify that a teacher can edit the details of an existing subject.
Teacher can indeed edit details of an existing subject

### Test Case 10: Teacher – Delete a Subject
Objective: Verify that a teacher can delete a subject they teach.
Teacher can indeed delete subject they teach

### Test Case 11: Data Persistence (Save and Load)
#### Objective: Verify that data is correctly saved and loaded from the JSON file.
The data is stored and loaded correctly

### Conclusion:
- All functional requirements were successfully validated.

- Data is being persisted and loaded correctly, and actions are reflected in the user interface as expected.

- No critical issues found during testing