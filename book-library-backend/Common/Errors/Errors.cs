namespace Domain.Errors;

public static class Errors
{
    public static readonly Error LoginFaild = new("Неверное имя пользователя и/или пароль.");
    public static readonly Error UserWithEmailAlreadyExists = new("Пользователь с таким Email уже существует.");
    public static readonly Error TokenInvalid = new("Некорректный токен.");
    public static readonly Error CollectionAlreadyExists = new("Коллекция уже существует.");
    public static readonly Error BookNotExists = new("Такой книги не существует.");
    public static readonly Error BookCreationFaild = new("Ошибка создания книги.");
    public static readonly Error RecommendationsFaild = new("Необходимо более десяти оценок для получения рекомендаций.");
}