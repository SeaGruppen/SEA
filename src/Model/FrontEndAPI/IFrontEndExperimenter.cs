namespace Model.FrontEndAPI;
using Model.Result;

public interface IFrontEndExperimenter {
    
    void StoreResultFromQuestion(IResult answer);
}