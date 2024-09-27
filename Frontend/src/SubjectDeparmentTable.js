import {getSubjectDepartments} from './subjectApi';
import { useState, useEffect } from 'react';

function SubjectDepartmentTable() {

    const [tableData, setTableData] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            const data = await getSubjectDepartments();
            setTableData(data);
        };
        fetchData();
    }, []);

    if (!tableData) return <div>Loading...</div>;
    
    return (
        <table className="subject-department-table"> 
            <thead>
                <tr>
                    <th>Department</th>
                    <th>Subject</th>
                    <th>ECTS</th>
                </tr>
            </thead>
            <tbody>
                {tableData.map((department) => (
                    department.subjects.map((subject, index) => (
                        <tr key={department.id + index}>
                            <td>{index === 0 ? department.name : ""}</td>
                            <td>{subject.name}</td>
                            <td>{subject.ectsPoints}</td>
                        </tr>
                    ))
                ))}
            </tbody>
        </table>
    );
}

export default SubjectDepartmentTable;