import SubjectTableRow from './SubjectTableRow';


function SubjectTable({subjects, handleUpdate, handleDelete}) {
    return (
        <table className="subject-table" title="Table Of Subjects">
          <thead>
            <tr>
              <th>Id</th> 
              <th>Name</th>
              <th>Ects</th>
              <th>DepartmentId</th>
              <th>TimeCreated</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
            {subjects.map((subject) => (
              <SubjectTableRow 
                key={subject.id}
                subject={subject}
                handleUpdate={handleUpdate}
                handleDelete={handleDelete}
              />
            ))}   
          </tbody>
        </table>
    );
}

export default SubjectTable;
