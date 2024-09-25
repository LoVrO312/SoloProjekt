import React, { useState } from 'react';
import TableRow from './TableRow';
import ActionButtons from './ActionButtons';


function SubjectTable({subjects, handleUpdate, handleDelete}) {
    return (
        <table className="subject-table" title="Table Of Subjects">
          <tr>
            <th>Id</th> 
            <th>Name</th>
            <th>Ects</th>
            <th>DepartmentId</th>
            <th>TimeCreated</th>
            <th>Action</th>
          </tr>
          {subjects.map((subject) => (
            <tr key={subject.id}>
                <TableRow subject={subject} />
                {console.log("current id: ", subject)}
                <ActionButtons 
                  subject={subject}
                  handleUpdate={handleUpdate}
                  handleDelete={handleDelete}
                />
            </tr>
          ))}   
        </table>
    );
}

export default SubjectTable;
