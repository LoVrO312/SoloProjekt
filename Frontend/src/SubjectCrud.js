import React, { useState, useEffect } from 'react';
import SubmitForm from './SubmitForm';
import SubjectTable from './SubjectTable';
import Modal from './Modal';
import SearchFilter from './SearchFilter';
import { getAllSubjectsFiltered, createSubject, deleteSubject, updateSubjectDepartment} from './subjectApi';

function SubjectCrud() {
  const [subject, setSubject] = useState({
    id: '',
    name: '',
    ectsPoints: '',
    departmentId: '',
    timeCreated: ''
  });

  const [subjects, setSubjects] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [subjectToUpdate, setSubjectToUpdate] = useState(null);
  const [searchFilter, setSearchFilter] = useState({
    filter: {
      searchQuery: '',
      departmentId: '',
      minEctsPoints: '',
      maxEctsPoints: '',
      fromTimeCreated: '',
      toTimeCreated: ''
    },
    sort: {
      sortBy: '',
      sortOrder: ''
    },
    page: {
      recordsPerPage: '',
      pageNumber: '1'
    }
  });
  const [refresh, setRefresh] = useState(false);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setSubject({ ...subject, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    await createSubject(subject);
    setRefresh(!refresh);
  };

  const handleUpdate = (subject) => {
    setSubjectToUpdate(subject); 
    setShowModal(true);          
  };

  const handleUpdateSubmit = async (e) => {
    e.preventDefault();
    await updateSubjectDepartment(subjectToUpdate.id, subjectToUpdate.departmentId);
    setShowModal(false);
    setRefresh(!refresh);
  };

  const handleModalInputChange = (e) => {
    const { name, value } = e.target;
    setSubjectToUpdate({ ...subjectToUpdate, [name]: value });
  };

  const handleDelete = async (id) => {
    await deleteSubject(id);
    setRefresh(!refresh);
  };

  useEffect(() => {
    const fetchSubjects = async () => {
      try {
        const fetchedSubjects = await getAllSubjectsFiltered({...searchFilter}); // is it necessary to spread?
        setSubjects(fetchedSubjects);
        localStorage.setItem('subjects', JSON.stringify(fetchedSubjects)); // Update localStorage REMOVE THIS LINE
      } catch (error) {
        console.error('Error fetching subjects:', error);
      }
    };
    fetchSubjects();
  }, [refresh, searchFilter]);

  const handleSearchInputChange = (e) => {
    const {name, value} = e.target;
    const [fspName, param] = name.split('.');

    setSearchFilter({...searchFilter, [fspName] : {
      ...searchFilter[fspName], [param] : value 
    }});
  };

  const handleSearchSubmit = (e) => {
    e.preventDefault();
    setRefresh(!refresh);
  };

  const handlePageChange = (newPageNumber) => {
    if (newPageNumber < 1) newPageNumber = 1;
  
    setSearchFilter({
      ...searchFilter,
      page: {
        ...searchFilter.page,
        pageNumber: newPageNumber
      }
    });
  };
  

  return (
    <>
      <div className="submit-form">
        <h1>Insert</h1>
        <SubmitForm
          subject={subject}
          handleInputChange={handleInputChange}
          handleSubmit={handleSubmit}
        />
      </div>
      <div className="search-filter">
        <h1>Search filter</h1>
        <SearchFilter
            filter={searchFilter.filter}
            sort={searchFilter.sort}
            page={searchFilter.page}
            handleSearchInputChange={handleSearchInputChange}
            handleSearchSubmit={handleSearchSubmit}
            handlePageChange={handlePageChange}
        />
      </div>
      <div>
        <h1>Table of subjects</h1>
        <SubjectTable 
          subjects={subjects}
          handleUpdate={handleUpdate}
          handleDelete={handleDelete}
        />
      </div>
      <Modal
        show={showModal}
        onClose={() => setShowModal(false)}
        subject={subjectToUpdate}
        handleInputChange={handleModalInputChange}
        handleUpdateSubmit={handleUpdateSubmit}
      />
    </>
  );
}

export default SubjectCrud;
