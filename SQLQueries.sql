-- Количество туров из Москвы
SELECT COUNT(Tours.TourId) FROM Tours
INNER JOIN Cities ON
Tours.FromCityId = Cities.CityId
WHERE Name = N'Москва';

-- Количество туров в Москву
SELECT COUNT(Tours.TourId) FROM Tours
INNER JOIN Cities ON
Tours.ToCityId = Cities.CityId
WHERE Name = N'Москва'

-- Количество туров из региона
SELECT COUNT(Tours.TourId) FROM Tours
INNER JOIN Cities 
ON Tours.FromCityId = Cities.CityId
WHERE RegionId = (SELECT Regions.RegionId from Regions WHERE Regions.Name = N'Приморский край')

-- Количество туров в региона
SELECT COUNT(Tours.TourId) FROM Tours
INNER JOIN Cities 
ON Tours.ToCityId = Cities.CityId
WHERE RegionId = (SELECT Regions.RegionId from Regions WHERE Regions.Name = N'Приморский край')

-- Количество туров из страны
SELECT COUNT(Tours.TourId) From Tours
INNER JOIN Cities
ON Tours.FromCityId = Cities.CityId
INNER JOIN Regions
ON Cities.RegionId = Regions.RegionId
WHERE CountryId = (SELECT Countries.CountryId from Countries WHERE Countries.Name = N'Испания')

-- Количество туров в страну
SELECT COUNT(Tours.TourId) From Tours
INNER JOIN Cities
ON Tours.ToCityId = Cities.CityId
INNER JOIN Regions
ON Cities.RegionId = Regions.RegionId
WHERE CountryId = (SELECT Countries.CountryId from Countries WHERE Countries.Name = N'Испания')