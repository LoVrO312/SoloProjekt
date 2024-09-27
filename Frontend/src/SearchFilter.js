import React from 'react'

function SearchFilter({ filter, sort, page, handleSearchInputChange, handleSearchSubmit, handlePageChange}) {
    return (
    <>
      <form onSubmit={handleSearchSubmit}>
          <label>Search Query:</label>
          <input
            type="text"
            name="filter.searchQuery"
            value={filter.searchQuery}
            placeholder="Search by name"
            onChange={handleSearchInputChange}
          />
        
          <label>Department ID:</label>
          <input
            type="text"
            name="filter.departmentId"
            value={filter.departmentId}
            placeholder="Department ID"
            onChange={handleSearchInputChange}
          />
        
          <label>Min ECTS Points:</label>
          <input
            type="number"
            name="filter.minEctsPoints"
            value={filter.minEctsPoints}
            placeholder="Minimum ECTS"
            onChange={handleSearchInputChange}
          />
        
          <label>Max ECTS Points:</label>
          <input
            type="number"
            name="filter.maxEctsPoints"
            value={filter.maxEctsPoints}
            placeholder="Maximum ECTS"
            onChange={handleSearchInputChange}
          />

        <div>
          <label>From Time Created:</label>
          <input
            type="date"
            name="filter.fromTimeCreated"
            value={filter.fromTimeCreated}
            onChange={handleSearchInputChange}
          />
        </div>
          <label>To Time Created:</label>
          <input
            type="date"
            name="filter.toTimeCreated"
            value={filter.toTimeCreated}
            onChange={handleSearchInputChange}
          />
        <div>
          <label>Sort By:</label>
          <select name="sort.sortBy" value={sort.sortBy} onChange={handleSearchInputChange}>
            <option value="">Select sort field</option>
            <option value="Subject name">Subject Name</option>
            <option value="Department name">Department Name</option>
            <option value="Number of ECTS">ECTS Points</option>
            <option value="Time created">Time Created</option>
          </select>
        </div>
          <label>Sort Order:</label>
          <select name="sort.sortOrder" value={sort.sortOrder} onChange={handleSearchInputChange}>
            <option value="Ascending">Ascending</option>
            <option value="Descending">Descending</option>
          </select>

          <label>Records Per Page:</label>
          <input
            type="number"
            name="page.recordsPerPage"
            value={page.recordsPerPage}
            placeholder="Records per page"
            onChange={handleSearchInputChange}
          />

          <label>Page Number: {page.pageNumber}</label>
        <button type="submit">Apply Filters</button> 
      </form>
      <button type="button" onClick={() => handlePageChange(parseInt(page.pageNumber)-1)}>Previous page</button>
      <button type="button" onClick={() => handlePageChange(parseInt(page.pageNumber)+1)}>Next page</button>
    </>
    );
  }
  

export default SearchFilter;

// name="newPageNumber" value="parseInt(page.pageNumber)+1" 